using System.ServiceModel;

namespace MicrosoftAzure.Tutorial.ServiceBus.Common
{
    public interface IProblemSolverChannel : IProblemSolver, IClientChannel
    {
    }
}
