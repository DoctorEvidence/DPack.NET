using Jering.Javascript.NodeJS;

using Microsoft.Extensions.DependencyInjection;

using System;
using System.IO;
using System.Threading.Tasks;

namespace DPack.NET
{
    public class DPack
    {
        private ServiceCollection _servicesCollection;
        private ServiceProvider _serviceProvider;
        private string _DPackJs;

        public DPack()
        {
            _servicesCollection = new ServiceCollection();
            _servicesCollection.AddNodeJS();
            _serviceProvider = _servicesCollection.BuildServiceProvider();
            _DPackJs = File.ReadAllText(@"..\node_modules\dpack\dist\index.js");
        }

        public async Task<string> Serialize(string input)
        {
            string output;
            try
            {
                output = await StaticNodeJSService.InvokeFromStringAsync<string>(_DPackJs, "serialize", args: new object[] { input });
            }
            catch (Exception ex)
            {
                output = ex.Message;
            }

            return output;
        }

        public async Task<string> Parse(string input)
        {
            string output;
            try
            {
                output = await StaticNodeJSService.InvokeFromStringAsync<string>(_DPackJs, "parse", args: new object[] { input });
            }
            catch (Exception ex)
            {
                output = ex.Message;
            }

            return output;

        }
    }
}
