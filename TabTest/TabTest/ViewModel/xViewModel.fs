namespace TabTest.ViewModel

open System
open System.Text
open System.Data
open System.Collections.ObjectModel
open System.Threading
open System.Diagnostics
open System.IO
//open System.ComponentModel
open System.Windows
open System.Windows.Controls
open System.Windows.Media

open ViewModule.FSharp

open Mpp.WindowsForms
open Mpp.TestTabItem
  

type ViewModel () as self =
    inherit ViewModule.ViewModelBase ()
    
    //let IsInDesignMode = DesignerProperties.GetIsInDesignMode (new DependencyObject ())

    let currentStatusString = "Current status: Idle"

    //let tabs = self.Factory.Backing ( <@ self.TabControlItems @>, new ObservableCollection<TestTabItem> () )    
    //let uiCtx = SynchronizationContext.Current
    
    let ipText = "Le Young Technologies -- Internal Use Only"

    let pathRoot =
        match Environment.MachineName with
        | "LYT-PURCHINE" -> @"C:\Users\mikep_offline\Documents\"
        | "ACMELAB" -> @"D:\"
        | x -> failwithf "Unsupported Machine Name '%s'." x
    
    //do
    //    tabs.Value.Add( TestTabItem("h1", "c1") )
    //    tabs.Value.Add( TestTabItem("h2", "c2") )

    member __.TabControlItems with get () = tabs.Value and set v = tabs.Value <- v
 
    member val ScriptFilepath = self.Factory.Backing ( <@ self.ScriptFilepath @>, Path.Combine (pathRoot, @"Projects\F# Projects\MppAutoBench\MppAutoBench\MeasureSeqs\AcDc-OOIO_Test01_mseq.csv") ) with get, set
     
    member __.SelectMeasurementSequence = self.Factory.CommandSync (fun _ ->
        let defaultMSDirectory = Path.Combine (pathRoot, @"Projects\F# Projects\MppAutoBench\MppAutoBench\MeasureSeqs")
        let initialDirectory =
            Path.GetDirectoryName self.ScriptFilepath.Value
            |> function
            | "" -> defaultMSDirectory
            | path -> path

        OpenSingleFileDialog {  InitialDirectory = initialDirectory
                                Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*"
                                FilterIndex = 1
                                RestoreDirectory = true
                                Title = "Select the Measurement Sequence to run..."
                                Filename = Path.GetFileName self.ScriptFilepath.Value
                                }
        |> function
        | Some fp -> self.ScriptFilepath.Value <- fp
        | None -> () )
         
    member __.OpenDatafileAsTemp = self.Factory.CommandAsync (fun _ ->
        async { return () }
        )
            
