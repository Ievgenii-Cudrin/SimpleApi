using SimpleApi.Enumeration;

namespace SimpleApi.DataTransfer.DeliveriesDto
{
    public class DeliveryCreateUpdateDto
    {
        public string Address { get; set; }

        public DeliveryType TypeDelivery { get; set; }
    }
}