using Online.Shop.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Online.Shop.Core.Services.Abstractions
{
    public interface IBarcodeService
    {
        Task<SaleResult> BarCodeSorgula(long barkodNo, CancellationToken cancellationToken);
    }
}
