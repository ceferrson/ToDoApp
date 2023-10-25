using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.Formula.Functions;
using ToDoAppNTIer.Common.Response;

namespace ToDoAppNTier.UI.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult ResponseRedirectToAction<T>(this Controller controller, IResponse<T> response, string actionName)
        {
            if (response.ResponseType == ResponseType.NotFound)
                return controller.NotFound();

            if(response.ResponseType == ResponseType.ValidationError)
            {
                if(response.ValidationErrors is not null)
                {
                    foreach (var error in response.ValidationErrors)
                    {
                        controller.ModelState.AddModelError(error.PropertyName, error.ErrorMesage);
                        return controller.View(response.Data);
                    }
                }
                //It can happen that there is no error related with validation but there is additional message. For example :
                // user try to create task which exists already in db. We don't have exact validation for it but when we met that situation we can send response message task already exists.
                controller.ModelState.AddModelError("", response.Message);

                return controller.View(response.Data);
            }
            return controller.RedirectToAction(actionName);
        }

        public static IActionResult ResponseView<T>(this Controller controller, IResponse<T> response)
        {
            if (response.ResponseType == ResponseType.NotFound)
                return controller.NotFound();
            return controller.View(response.Data);
        }

        public static IActionResult ResponseRedirectToAction(this Controller controller, IResponse response, string actionName)
        {
            if(response.ResponseType == ResponseType.NotFound)
                return controller.NotFound();
            return controller.RedirectToAction(actionName);
        }
    }
}
