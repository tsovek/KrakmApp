export class Route {
    public Id: number;
    public Name: string;
    public Description: string;
    public Active: boolean;
    public Points: RoutePoint[];

    constructor() {

    }
}

export class RoutePoint {
    public Order: number;
    public Name: string;
    public Description: string;
    public ImageUrl: string;
    public MarkerUrl: string;
    
}