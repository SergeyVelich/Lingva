using Lingva.BC.Common.Enums;
using Lingva.MVC.Models.Request;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lingva.MVC.Infrastructure
{

    public class OptionsModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            // get data from query by values privider
            var filtersPartValues = bindingContext.ValueProvider.GetValue("filters");
            var sortersPartValues = bindingContext.ValueProvider.GetValue("sorters");
            var pagePartValue = bindingContext.ValueProvider.GetValue("page");
            var pageSizePartValue = bindingContext.ValueProvider.GetValue("pagesize");

            // get values
            string filters = filtersPartValues.FirstValue;
            string sorters = sortersPartValues.FirstValue;
            string page = pagePartValue.FirstValue;
            string pageSize = pageSizePartValue.FirstValue;

            OptionsModel model = (OptionsModel)bindingContext.Model ?? new OptionsModel();
            model.Filters = GetFiltersFromQuery(filters);
            model.Sorters = GetSortersFromQuery(sorters);
            model.Pagenator = GetPagenatorFromQuery(page, pageSize);

            // set binding result
            bindingContext.Result = ModelBindingResult.Success(model);
            return Task.CompletedTask;
        }

        private Dictionary<string, FilterModel> GetFiltersFromQuery(string filtersString)
        {
            Dictionary<string, FilterModel> result = new Dictionary<string, FilterModel>();

            if (filtersString == null)
            {
                return result;
            }           

            string[] filtersArr = filtersString.Split(';');
            foreach (string filter in filtersArr)
            {
                string[] parts = filter.Split(',');
                FilterModel filterModel = new FilterModel
                {
                    PropertyName = parts[0],
                    Operation = Enum.Parse<FilterOperation>(parts[1]),
                    PropertyValue = parts[2]
                };
                result.Add(parts[0], filterModel);
            }

            return result;
        }

        private Dictionary<string, SorterModel> GetSortersFromQuery(string sortersString)
        {
            Dictionary<string, SorterModel> result = new Dictionary<string, SorterModel>();

            if (sortersString == null)
            {
                return result;
            }
           
            string[] sortersArr = sortersString.Split(';');
            foreach(string sorter in sortersArr)
            {
                string[] parts = sorter.Split(',');
                SorterModel sorterModel = new SorterModel
                {
                    PropertyName = parts[0],
                    SortOrder = Enum.Parse<SortOrder>(parts[1])
                };
                sorterModel.SorterString = sorterModel.PropertyName + " " + sorterModel.SortOrder.ToString();
                result.Add(parts[0], sorterModel);
            }

            return result;
        }

        private PagenatorModel GetPagenatorFromQuery(string page, string pageSize)
        {
            if (string.IsNullOrEmpty(page) || !Int32.TryParse(page, out int pageNumber))
            {
                pageNumber = 1;
            }

            if (string.IsNullOrEmpty(pageSize) || !Int32.TryParse(pageSize, out int pageSizeNumber))
            {
                pageSizeNumber = 3;
            }

            PagenatorModel result = new PagenatorModel
            {
                PageSize = pageSizeNumber,
                CurrentPage = pageNumber
            };

            return result;
        }
    }
}
