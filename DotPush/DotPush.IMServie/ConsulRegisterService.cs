using Consul;
using DotPush.IMServie.Models;

namespace DotPush.IMServie
{
    /// <summary>
    /// 注册服务
    /// </summary>
    public class ConsulRegisterService : IHostedService
    {
        private IConsulClient _consulClient;
        private ServiceInfoConfig _serviceInfo;
        private IWebHostEnvironment _env;

        public ConsulRegisterService(IConsulClient consulClient, ServiceInfoConfig config, IWebHostEnvironment environment)
        {
            _consulClient = consulClient;
            _serviceInfo = config;
            _env = environment;
            if (String.IsNullOrEmpty(_serviceInfo.Id))
            {
                _serviceInfo.Id =$"{_serviceInfo.IP}:{_serviceInfo.Port}";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine($"start to register service {_serviceInfo.Name} to consul client ...");
            await _consulClient.Agent.ServiceDeregister(_serviceInfo.Name, cancellationToken);

            var meta = new Dictionary<string, string>();
            meta.Add("IMPort", _serviceInfo.IMPort.ToString());
            await _consulClient.Agent.ServiceRegister(new AgentServiceRegistration
            {
                ID= _serviceInfo.Id,
                Name = _serviceInfo.Name,// 服务名
                Address = _serviceInfo.IP, // 服务绑定IP
                Port = _serviceInfo.Port, // 服务绑定端口
                Check = new AgentServiceCheck()
                {
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(0),//服务启动多久后注册
                    Interval = TimeSpan.FromSeconds(5),//健康检查时间间隔
                    HTTP = $"http://{_serviceInfo.IP}:{_serviceInfo.Port}/" + _serviceInfo.HealthCheckAddress,//健康检查地址
                    Timeout = TimeSpan.FromSeconds(5)
                },
                Meta = meta
            });
            Console.WriteLine("register service info to consul client Successful ...");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _consulClient.Agent.ServiceDeregister(_serviceInfo.Id, cancellationToken);
            Console.WriteLine($"Deregister service {_serviceInfo.Id} from consul client Successful ...");
        }
    }
}
