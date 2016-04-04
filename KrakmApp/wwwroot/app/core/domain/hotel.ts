export class Hotel {
    Id: number;
    Name: string;
    Latitude: number;
    Longitude: number;
    Adress: string;
    Phone: string;
    Email: string;

    constructor(
        id: number,
        name: string,
        latitude: number,
        longitude: number,
        adress: string,
        phone: string,
        email: string) {
        this.Id = id;
        this.Name = name;
        this.Latitude = latitude;
        this.Longitude = longitude;
        this.Adress = adress;
        this.Phone = phone;
        this.Email = email;
    }
}