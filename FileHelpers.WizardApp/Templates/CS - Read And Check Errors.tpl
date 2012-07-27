
var engine = new FileHelperEngine<${ClassName}>();

engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

var res = engine.ReadFile(@"YourFile.txt");

if (engine.ErrorManager.ErrorCount > 0) 
   engine.ErrorManager.SaveErrors("Errors.txt");
