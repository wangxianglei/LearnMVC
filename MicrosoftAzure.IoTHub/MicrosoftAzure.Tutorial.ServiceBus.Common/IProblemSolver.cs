using System.ServiceModel;

namespace MicrosoftAzure.Tutorial.ServiceBus.Common
{
    [ServiceContract(Namespace ="urn:ps")]
    public interface IProblemSolver
    {
        [OperationContract]
        int AddNumbers(int a, int b);
    }

    public interface IProblemSolverChannel : IProblemSolver, IClientChannel
    { }

}
