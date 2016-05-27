export class Partner {
    Id: number;
    Name: string;
    Latitude: number;
    Longitude: number;
    Description: string;
    Phone: string;
    Commission: number;
    PromotionKind: number;
    StartPromotion: Date;
    EndPromotion: Date;
    ImageUrl: string;

    constructor(
        id: number,
        name: string,
        latitude: number,
        longitude: number,
        description: string,
        phone: string,
        commission: number,
        promotionKind: number,
        startPromotion: Date,
        endPromotion: Date,
        imageUrl: string) {
        this.Id = id;
        this.Name = name;
        this.Latitude = latitude;
        this.Longitude = longitude;
        this.Description = description;
        this.Phone = phone;
        this.Commission = commission;
        this.PromotionKind = promotionKind;
        this.StartPromotion = startPromotion;
        this.EndPromotion = endPromotion;
        this.ImageUrl = imageUrl;
    }
}