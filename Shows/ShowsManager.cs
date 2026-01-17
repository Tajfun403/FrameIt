using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FrameIt.Shows;

class ShowsManager
{
    public ObservableCollection<PhotoShow> Shows { get; private set; } = [];

    public static ShowsManager Instance { get; } = new ShowsManager();

    public void GenerateDefaultShows() 
    {
        PhotoShow defaultShow = new();
        defaultShow.PhotosList.Add(new ShowImage()
        {
            ImagePath = "Images/Liara.jpg",
            DisplayName = "Sample Photo 1"
        });
        defaultShow.PhotosList.Add(new ShowImage()
        {
            ImagePath = "Images/GrayLiara.jpg",
            DisplayName = "Sample Photo 2"
        });
        defaultShow.DisplayName = "Sample PhotoShow";

        Shows.Add(defaultShow);
    }


}
