using System.Collections.Generic;
using System.Collections.ObjectModel;
using PKB.DomainModel.Common;
using PKB.DomainModel.Model;
using PKB.DomainModel.Repositories;
using PKB.WPF.Views.SectionTree;

namespace PKB.WPF.Design
{
    public static class SampleData
    {
        public static ResourceViewModel MakeResource()
        {
            var resource = ResourceRepository.TestResource;

            var book = new ResourceViewModel(resource.Id, resource.Name);
            f(book.Sections,resource.Sections);
            return book;
        }

        private static void f(ObservableCollection<SectionViewModel> collection, IEnumerable<Section> sections)
        {
            foreach (var s in sections)
            {
                var newS = new SectionViewModel(s.Id, s.Name);
                collection.Add(newS);
                f(newS.Subsections, s.Subsections);
            }


        }
    }
}
