namespace DotPush.IMServie.Models
{
    /// <summary>
    /// 服务信息配置
    /// </summary>
    public class ServiceInfoConfig
    {
        /// <summary>
        /// 服务id
        /// </summary>
        public string Id { get; set; } = "";
        /// <summary>
        /// 服务名称
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// 服务ip地址
        /// </summary>
        public string IP { get; set; } = "";
        /// <summary>
        /// 服务端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// IM 端口
        /// </summary>
        public int IMPort { get; set; }

        /// <summary>
        /// 心跳检测地址
        /// </summary>
        public string HealthCheckAddress { get; set; } = "";

        /// <summary>
        /// Consul注册地址
        /// </summary>
        public string ConsulAddress { get; set; } = "";
    }
}
