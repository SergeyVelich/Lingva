using Lingva.BC.Common.Enums;

namespace Lingva.MVC.Models.Response
{
    public class SortViewModel
    {
        public SortState NameSort { get; set; } // значение для сортировки по имени
        public SortState DescriptionSort { get; set; }    // значение для сортировки по возрасту
        public SortState PictureSort { get; set; }   // значение для сортировки по компании
        public SortState Current { get; set; }     // значение свойства, выбранного для сортировки
        public bool Up { get; set; }  // Сортировка по возрастанию или убыванию

        public SortViewModel(SortState sortOrder)
        {
            // значения по умолчанию
            NameSort = SortState.NameAsc;
            DescriptionSort = SortState.DescriptionAsc;
            PictureSort = SortState.PictureAsc;
            Up = true;

            if (sortOrder == SortState.DescriptionDesc || sortOrder == SortState.NameDesc
                || sortOrder == SortState.PictureDesc)
            {
                Up = false;
            }

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    Current = NameSort = SortState.NameAsc;
                    break;
                case SortState.DescriptionAsc:
                    Current = DescriptionSort = SortState.DescriptionDesc;
                    break;
                case SortState.DescriptionDesc:
                    Current = DescriptionSort = SortState.DescriptionAsc;
                    break;
                case SortState.PictureAsc:
                    Current = PictureSort = SortState.PictureDesc;
                    break;
                case SortState.PictureDesc:
                    Current = PictureSort = SortState.PictureAsc;
                    break;
                default:
                    Current = NameSort = SortState.NameDesc;
                    break;
            }
        }
    }
}
