
FileHelperEngine engine = new FileHelperEngine(${ClassName});

engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;

${ClassName}[] res = (${ClassName}[]) engine.ReadFile(@"YourFile.txt");

if (engine.ErrorManager.ErrorCount > 0) 
   engine.ErrorManager.SaveErrors("Errors.txt");
