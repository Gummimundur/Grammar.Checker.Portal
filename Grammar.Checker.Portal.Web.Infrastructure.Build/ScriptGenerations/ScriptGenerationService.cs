using ADotNet.Clients;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s;

namespace Grammar.Checker.Portal.Web.Infrastructure.Build.ScriptGenerations
{
    public class ScriptGenerationService
    {
        private readonly ADotNetClient adotNetClient;

        public ScriptGenerationService() =>
            this.adotNetClient = new ADotNetClient();

        public void GenerateBuildScript()
        {
            var githubPipeline = new GithubPipeline
            {
                Name = "Grammar checker portal build",

                OnEvents = new Events
                {
                    Push = new PushEvent
                    {
                        Branches = new string[] { "main" }
                    },

                    PullRequest = new PullRequestEvent
                    {
                        Branches = new string[] { "main" }
                    }
                },

                Jobs = new Jobs
                {
                    Build = new BuildJob
                    {
                        RunsOn = BuildMachines.Windows2022,
                        Steps = new List<GithubTask>
            {
                new CheckoutTaskV2
                {
                    Name = "Checking out code"
                },

                new SetupDotNetTaskV1
                {
                    Name = "Installing .Net",

                    TargetDotNetVersion = new TargetDotNetVersion
                    {
                        DotNetVersion = "7.0.100-preview.4.22252.9",
                        IncludePrerelease = true
                    }
                },

                new RestoreTask
                {
                    Name = "Restoring packages"
                },

                new DotNetBuildTask
                {
                    Name = "Building Project(s)"
                },

                new TestTask
                {
                    Name = "Running Tests"
                }
            }
                    }
                }
            };

            this.adotNetClient.SerializeAndWriteToFile(
                githubPipeline,
                "../../../../.github/workflows/dotnet.yml");
        }

        public void GenerateProvisionScript()
        {
            var githubPipeline = new GithubPipeline
            {
                Name = "Provision Grammar Checker Portal",

                OnEvents = new Events
                {
                    Push = new PushEvent
                    {
                        Branches = new string[] { "main" }
                    },

                    PullRequest = new PullRequestEvent
                    {
                        Branches = new string[] { "main" }
                    }
                },

                Jobs = new Jobs
                {
                    Build = new BuildJob
                    {
                        RunsOn = BuildMachines.WindowsLatest,

                        EnvironmentVariables = new Dictionary<string, string>
                        {
                            { "AzureClientId", "${{ secrets.AZURECLIENTID }}" },
                            { "AzureClientSecret", "${{ secrets.AZURECLIENTSECRET }}" },
                            { "AzureTenantId", "${{ secrets.AZURETENANTID }}" },
                            { "AzureAdminName", "${{ secrets.AZUREADMINNAME }}" },
                            { "AzureAdminAccess", "${{ secrets.AZUREADMINACCESS }}" },
                            { "ExternalTextAnalyzerUrl", "${{ secrets.EXTERNALTEXTANALYZERURL }}" },
                            { "SyncFusionLicenseKey", "${{ secrets.SYNCFUSIONLICENSEKEY }}" }
                        },

                        Steps = new List<GithubTask>
                        {
                            new CheckoutTaskV2
                            {
                                Name = "Check out"
                            },

                            new SetupDotNetTaskV1
                            {
                                Name = "Setup Dot Net Version",

                                TargetDotNetVersion = new TargetDotNetVersion
                                {
                                    DotNetVersion = "7.0.100-preview.4.22252.9",
                                    IncludePrerelease = true,
                                }
                            },

                            new RestoreTask
                            {
                                Name = "Restore"
                            },

                            new DotNetBuildTask
                            {
                                Name = "Build"
                            },

                            new RunTask
                            {
                                Name = "Provision",
                                Run = "dotnet run --project .\\Grammar.Checker.Portal.Web.Infrastructure.Provision\\Grammar.Checker.Portal.Web.Infrastructure.Provision.csproj"
                            }
                        }
                    }
                }
            };

            this.adotNetClient.SerializeAndWriteToFile(
                githubPipeline,
                path: "../../../../.github/workflows/provision.yml");
        }
    }
}