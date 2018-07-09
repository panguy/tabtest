namespace TabTest.ViewModel

open System.Windows.Controls

open ViewModule.FSharp
open System.Collections.ObjectModel


// https://www.wpftutorial.net/datatemplates.html


type ViewModel () as self =
    inherit ViewModule.ViewModelBase ()
    
    let tabControlItems = self.Factory.Backing ( <@ self.TabControlItems @>, ObservableCollection<TabItem>() )
    
    member val Header = "" with get, set
    member val Content = "" with get, set

    member __.TabControlItems with get () = tabControlItems.Value and set v = tabControlItems.Value <- v

        