export class Hotel {
    Name: string;
    Latitude: number;
    Longitude: number;
    Description: string;

    constructor(
        name: string,
        latitude: number,
        longitude: number,
        description: string) {
        this.Name = name;
        this.Latitude = latitude;
        this.Longitude = longitude;
        this.Description = description;
    }
}