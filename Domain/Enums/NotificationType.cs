namespace Domain.Enums;

public enum NotificationType
{
    OrderPlaced = 0,
    OrderConfirmed = 1,
    OrderShipped = 2,
    OrderDelivered = 3,
    OrderCancelled = 4,
    PaymentFailed = 5,
    LowStockAlert = 6,
    ReviewApproved = 7,
    PromotionAlert = 8,
}