export class Banner {
    Id: number;
    Name: string;
    Description: string;
    StartPromotion: Date;
    EndPromotion: Date;
    ImageUrl: string;

    constructor(
        id: number,
        name: string,
        description: string,
        startPromotion: Date,
        endPromotion: Date,
        imageUrl: string) {
        this.Id = id;
        this.Name = name;
        this.Description = description;
        this.StartPromotion = startPromotion;
        this.EndPromotion = endPromotion;
        this.ImageUrl = imageUrl;
    }
}