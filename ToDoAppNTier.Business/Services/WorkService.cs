using AutoMapper;
using FluentValidation;
using ToDoAppNTier.Business.Extensions;
using ToDoAppNTier.Business.Interfaces;
using ToDoAppNTier.DataAccess.UnitOfWork;
using ToDoAppNTier.Dtos.Dtos;
using ToDoAppNTier.Entities.Domains;
using ToDoAppNTIer.Common.Response;

namespace ToDoAppNTier.Business.Services
{
    public class WorkService : IWorkService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<WorkDto> _workDtoValidator;
        private readonly IValidator<WorkCreateDto> _createDtoValidator;

        public WorkService(IUow uow, IMapper mapper, IValidator<WorkDto> workDtoValidator, IValidator<WorkCreateDto> createDtoValidator) 
        { 
            _uow = uow;
            _mapper = mapper;
            _workDtoValidator = workDtoValidator;
            _createDtoValidator = createDtoValidator;
        }

        public async Task<IResponse<List<WorkDto>>> GetAll()
        {
            var workList = _mapper.Map<List<WorkDto>>(await _uow.GetRepository<Work>().GetAll());
            return new Response<List<WorkDto>>(ResponseType.Succes, workList);
        }

        public async Task<IResponse<WorkCreateDto>> Create(WorkCreateDto workDto)
        {
            var validator = _createDtoValidator.Validate(workDto);
            if(validator.IsValid)
            {
                //check that does work already exists or not:
                var workInDb = _uow.GetRepository<Work>().GetQueryable().Where(wdb => wdb.Title.Trim() == workDto.Title.Trim()).FirstOrDefault();
                if(workInDb == null)
                {
                    //convert from dto to original entity
                    var work = _mapper.Map<Work>(workDto);
                    await _uow.GetRepository<Work>().Create(work);
                    await _uow.Save();
                    return new Response<WorkCreateDto>(ResponseType.Succes, workDto);
                }
                else
                {
                    return new Response<WorkCreateDto>(ResponseType.ValidationError, "Work Title already exists!");
                }
            }
            else
            {
                var errors = validator.ConvertToCustomValidationError();
                return new Response<WorkCreateDto>(ResponseType.ValidationError, errors, workDto);
            }
        }

        public async Task<IResponse<WorkDto>> GetById(int id)
        {
            var work = _mapper.Map<WorkDto>(await _uow.GetRepository<Work>().GetByFilter(w => w.Id == id));
            if (work == null)
                return new Response<WorkDto>(ResponseType.NotFound, "Work couldn't find!");
            return new Response<WorkDto>(ResponseType.Succes, work);
        }

        public async Task<IResponse> Remove(int id)
        {
            var removedEntity = await _uow.GetRepository<Work>().GetByFilter(w => w.Id == id);

            if(removedEntity != null)
            {
                _uow.GetRepository<Work>().Remove(removedEntity);
                await _uow.Save();
                return new Response(ResponseType.Succes);
            }
            return new Response(ResponseType.NotFound, "Removed entity couldn't found!");
        }

        public async Task<IResponse<WorkDto>> Update(WorkDto workUpdateDto)
        {
            //Check validation of work dto 
            var validator = _workDtoValidator.Validate(workUpdateDto);
            
            if (validator.IsValid)
            {
                //Check that is there work in db like this at all.
                var workInDb = await _uow.GetRepository<Work>().GetById(workUpdateDto.Id);
                if(workInDb != null)
                {
                    //Mapping of dto to real entity
                    var work = _mapper.Map<Work>(workUpdateDto);
                    _uow.GetRepository<Work>().Update(work, workInDb);
                    await _uow.Save();
                    return new Response<WorkDto>(ResponseType.Succes, workUpdateDto);
                }
                else
                {
                    return new Response<WorkDto>(ResponseType.NotFound, workUpdateDto);
                }
            } 
            else
            {
                var errors = validator.ConvertToCustomValidationError();
                return new Response<WorkDto>(ResponseType.ValidationError, errors, workUpdateDto);
            }
        }
    }
}
