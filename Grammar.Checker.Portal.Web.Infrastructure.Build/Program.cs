using Grammar.Checker.Portal.Web.Infrastructure.Build.ScriptGenerations;

var scriptGenerationService = new ScriptGenerationService();
scriptGenerationService.GenerateBuildScript();
scriptGenerationService.GenerateProvisionScript();