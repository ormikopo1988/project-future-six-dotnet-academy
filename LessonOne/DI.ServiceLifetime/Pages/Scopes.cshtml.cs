using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DI.ServiceLifetime.Pages
{
    public class ScopesModel : PageModel
    {
        private readonly IOperationTransient _transientOperation;
        private readonly IOperationScoped _scopedOperation;
        private readonly IOperationSingleton _singletonOperation;
        
        public ScopesModel(IOperationTransient transientOperation,
            IOperationScoped scopedOperation,
            IOperationSingleton singletonOperation)
        {
            _transientOperation = transientOperation;
            _scopedOperation = scopedOperation;
            _singletonOperation = singletonOperation;
        }

        public string TransientOperationId { get; set; }

        public string ScopedOperationId { get; set; }
        
        public string SingletonOperationId { get; set; }

        public void OnGet()
        {
            TransientOperationId = _transientOperation.OperationId;
            ScopedOperationId = _scopedOperation.OperationId;
            SingletonOperationId = _singletonOperation.OperationId;
        }
    }
}
