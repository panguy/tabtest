module MainApp

open System
open System.Windows
open System.Threading

open log4net.Util
open System.Windows.Threading



let private InitLogging (configName : string)
                        (logName : string) =
    
    let configFileInfo = System.IO.FileInfo (configName)
    let debug =
        log4net.Config.XmlConfigurator.Configure (configFileInfo)
        |> Seq.cast<LogLog>
        |> Seq.toArray

    if debug.Length > 0 then failwithf "Error(s) configuring the logger system:%s%A" Environment.NewLine debug

        
// Application Entry point
[<STAThread>]
[<EntryPoint>]
let main _ =
    InitLogging "log4net.config" "logfile"
    
    if SynchronizationContext.Current = null then
        DispatcherSynchronizationContext (Dispatcher.CurrentDispatcher)
        |> SynchronizationContext.SetSynchronizationContext

    // Create the View and bind it to the View Model
    let mainWindowViewModel = System.Windows.Application.LoadComponent (new System.Uri ("/TabTest;component/mainwindow.xaml", UriKind.Relative)) :?> Window
    //mainWindowViewModel.DataContext <- new ViewModel () 
    
    (new Application()).Run (mainWindowViewModel)