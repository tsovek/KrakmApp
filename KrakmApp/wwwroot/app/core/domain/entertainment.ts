export class Entertainment {
    Id: number;
    Name: string;
    Latitude: number;
    Longitude: number;
    ImageUrl: string;
    Payable: boolean;
    Description: string;
    Adress: string;

    constructor(
        id: number,
        name: string,
        latitude: number,
        longitude: number,
        imageUrl: string,
        payable: boolean,
        description: string,
        adress: string) {
        this.Id = id;
        this.Name = name;
        this.Latitude = latitude;
        this.Longitude = longitude;
        this.ImageUrl = imageUrl;
        this.Payable = payable;
        this.Description = description;
        this.Adress = adress;
    }
}