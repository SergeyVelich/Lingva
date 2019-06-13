using Lingva.ASP.Infrastructure.Models;
using Lingva.Common.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

namespace Lingva.ASP.Infrastructure.Binders
{

    public class GroupsListOptionsModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            // get data from query by values privider
            var filterNameValue = bindingContext.ValueProvider.GetValue("name");
            var filterLanguageValue = bindingContext.ValueProvider.GetValue("languageId");
            var filterDateFromValue = bindingContext.ValueProvider.GetValue("dateFrom");
            var filterDateToValue = bindingContext.ValueProvider.GetValue("dateTo");
            var sortOrderValue = bindingContext.ValueProvider.GetValue("sortOrder");
            var sortPropertyValue = bindingContext.ValueProvider.GetValue("sortProperty");
            var pageIndexValue = bindingContext.ValueProvider.GetValue("pageIndex");
            var pageSizeValue = bindingContext.ValueProvider.GetValue("pageSize");

            // get values
            string filterName = filterNameValue.FirstValue;
            int.TryParse(filterLanguageValue.FirstValue, out int filterLanguage);
            DateTime.TryParse(filterDateFromValue.FirstValue, out DateTime filterDateFrom);
            filterDateFrom = filterDateFrom.ToUniversalTime().AbsoluteStart();
            DateTime.TryParse(filterDateToValue.FirstValue, out DateTime filterDateTo);
            filterDateTo = filterDateTo == DateTime.MinValue ? DateTime.MaxValue : filterDateTo.ToUniversalTime().AbsoluteEnd();
            string sortProperty = sortPropertyValue.FirstValue ?? "Name";
            string sortOrder = sortOrderValue.FirstValue ?? "Desc";

            int pageIndex = int.TryParse(pageIndexValue.FirstValue, out pageIndex) ? pageIndex : 1;
            int pageSize = int.TryParse(pageSizeValue.FirstValue, out pageSize) ? pageSize : 5;

            //fill properties           
            GroupsListOptionsModel model = (GroupsListOptionsModel)bindingContext.Model ?? new GroupsListOptionsModel();
            model.Name = filterName;
            model.LanguageId = filterLanguage;
            model.DateFrom = filterDateFrom;
            model.DateTo = filterDateTo;
            model.SortProperty = sortProperty;
            model.SortOrder = sortOrder;
            model.PageIndex = pageIndex;
            model.PageSize = pageSize;

            // set binding result
            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }
    }
}


