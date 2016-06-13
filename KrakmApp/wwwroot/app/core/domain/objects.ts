
export class Objects {
    public Objects: Array<ObjectGroup>;

    constructor(objectGroup: Array<ObjectGroup>) {
        this.Objects = objectGroup; 
    }
}

export class ObjectGroup {
    public Type: string;
    public SingleObjects: Array<SingleObject>;

    constructor(typeName: string, singleObjects: Array<SingleObject>) {
        this.Type = typeName;
        this.SingleObjects = singleObjects;
    }
}

export class SingleObject {
    Id: number;
    Name: string;
    Description: string;
    ImageUrl: string;
    Latitude: number;
    Longitude: number;

    constructor(
        id: number,
        name: string,
        description: string,
        imageUrl: string,
        latitude: number,
        longitude: number) {
        this.Id = id;
        this.Name = name;
        this.Latitude = latitude;
        this.Longitude = longitude;
        this.Description = description;
        this.ImageUrl = imageUrl;
    }
}