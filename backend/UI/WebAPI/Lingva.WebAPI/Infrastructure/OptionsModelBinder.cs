using Lingva.WebAPI.Models.Request;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

namespace Lingva.WebAPI.Infrastructure
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
            //var filtersPartValues = bindingContext.ValueProvider.GetValue("filters");
            //var sortersPartValues = bindingContext.ValueProvider.GetValue("sorters");
            //var pagePartValue = bindingContext.ValueProvider.GetValue("page");
            //var pageSizePartValue = bindingContext.ValueProvider.GetValue("pagesize");

            // get values
            //string filters = filtersPartValues.FirstValue;
            //string sorters = sortersPartValues.FirstValue;
            //string page = pagePartValue.FirstValue;
            //string pageSize = pageSizePartValue.FirstValue;

            GroupsListOptionsModel options = new GroupsListOptionsModel();
            //options.Filters = GetFiltersFromQuery(filters);
            //options.Sorters = GetSortersFromQuery(sorters);
            //options.Pagenator = GetPagenatorFromQuery(page, pageSize);

            // set binding result
            bindingContext.Result = ModelBindingResult.Success(options);
            return Task.CompletedTask;
        }

        //private ICollection<FilterModel> GetFiltersFromQuery(string filtersString)
        //{
        //    List<FilterModel> result = new List<FilterModel>();

        //    if (filtersString == null)
        //    {
        //        return result;
        //    }           

        //    string[] filtersArr = filtersString.Split(';');
        //    foreach (string filter in filtersArr)
        //    {
        //        string[] parts = filter.Split(',');
        //        FilterModel filterModel = new FilterModel
        //        {
        //            PropertyName = parts[0],
        //            Operation = Enum.Parse<FilterOperation>(parts[1]),
        //            PropertyValue = parts[2]
        //        };
        //        result.Add(filterModel);
        //    }

        //    return result;
        //}

        //private ICollection<SorterModel> GetSortersFromQuery(string sortersString)
        //{
        //    List<SorterModel> result = new List<SorterModel>();

        //    if (sortersString == null)
        //    {
        //        return result;
        //    }
           
        //    string[] sortersArr = sortersString.Split(';');
        //    foreach(string sorter in sortersArr)
        //    {
        //        string[] parts = sorter.Split(',');
        //        SorterModel sorterModel = new SorterModel
        //        {
        //            PropertyName = parts[0],
        //            SortOrder = Enum.Parse<SortOrder>(parts[1])
        //        };
        //        sorterModel.SorterString = sorterModel.PropertyName + " " + sorterModel.SortOrder.ToString();
        //        result.Add(sorterModel);
        //    }

        //    return result;
        //}

        //private PagenatorModel GetPagenatorFromQuery(string page, string pageSize)
        //{
        //    if (string.IsNullOrEmpty(page) || !Int32.TryParse(page, out int pageNumber))
        //    {
        //        pageNumber = 1;
        //    }

        //    if (string.IsNullOrEmpty(pageSize) || !Int32.TryParse(pageSize, out int pageSizeNumber))
        //    {
        //        pageSizeNumber = 5;
        //    }

        //    PagenatorModel result = new PagenatorModel
        //    {
        //        PageSize = pageSizeNumber,
        //        CurrentPage = pageNumber
        //    };

        //    return result;
        //}

        //private void EnsurePropertyExists(string propertyName)//??
        //{
        //    //var propertyExist = ValidateModelType.HasProperty(propertyName);

        //    //if (!propertyExist)
        //    //{
        //    //    throw new InvalidModelException($"Filter{propertyName} is not acceptable for model {typeof(TValidateModel).Name}");
        //    //}
        //}

    }
}
