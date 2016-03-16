export class Partner {
    Id: number;
    Name: string;
    Latitude: number;
    Longitude: number;
    Description: string;

    constructor(
        id: number,
        name: string,
        latitude: number,
        longitude: number,
        description: string) {
        this.Id = id;
        this.Name = name;
        this.Latitude = latitude;
        this.Longitude = longitude;
        this.Description = description;
    }
}