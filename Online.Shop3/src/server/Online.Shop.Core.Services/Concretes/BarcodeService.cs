using Microsoft.Extensions.Caching.Distributed;
using Online.Shop.Core.Contracts;
using Online.Shop.Core.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Online.Shop.Core.Services.Concretes
{
    public class BarcodeService : IBarcodeService
    {
        private readonly HttpClient httpClient;
        private readonly IDistributedCache cache;
        private readonly string userName;
        private readonly string password;

        public BarcodeService(HttpClient httpClient, IDistributedCache cache)
        {
            this.httpClient = httpClient;
            this.cache = cache;
            this.userName = "test";
            this.password = "test";
        }
        public async Task<SaleResult> BarCodeSorgula(long barkodNo, CancellationToken cancellationToken)
        {
            var cacheKey = "REDIS-SHOP"+barkodNo;
            var strBarcodeResult = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(strBarcodeResult))
            {
                return JsonSerializer.Deserialize<SaleResult>(strBarcodeResult, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            string token = await GetToken(cancellationToken);
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            strBarcodeResult = await httpClient.GetStringAsync($"kjdkdlkdld");
            await cache.SetStringAsync(cacheKey, strBarcodeResult, new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) }, cancellationToken);
            return JsonSerializer.Deserialize<SaleResult>(strBarcodeResult, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }      



        private async Task<string> GetToken(CancellationToken cancellationToken)
        {
            var cacheKey = "REDIS-TOKEN";
            var token = await cache.GetStringAsync(cacheKey);
            if (!string.IsNullOrEmpty(token))
            {
                return token;
            }
            var postData = new { UserName = userName, Password = password };
            var strPostData = JsonSerializer.Serialize(postData);
            var body = new StringContent(strPostData, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync($"baseasress", body, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException("error!");
            }
            var strResponse = await response.Content.ReadAsStringAsync();
            var tokenResult = JsonSerializer.Deserialize<TokenResult>(strResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            await cache.SetStringAsync(cacheKey, tokenResult.Token, new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) }, cancellationToken);
            return tokenResult.Token;
        }
    }
}

