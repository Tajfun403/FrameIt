using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FrameIt.Shows;

class ShowsManager
{
    public ObservableCollection<PhotoShow> Shows { get; private set; } = [];
    public static ShowsManager Instance { get; } = new ShowsManager();

    protected void GenerateDefaultShows() { }


}
