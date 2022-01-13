namespace Shared
{
    // One big "Viewmodel" that has properties to match sub-view models, which means you can use to viewmodels in the same view instead of using partial views. One of 5 ways to do it.
    public class ViewModelsCVSite
    {
        public ProjectCreateModel ProjectModel { get; set; }

        public CVDetailModel CvModel { get; set; }

        public CreateMessagesViewModel CreateMessagesViewModel { get; set; }

    }
}
