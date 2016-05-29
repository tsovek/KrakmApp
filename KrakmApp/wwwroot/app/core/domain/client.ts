export class Client {
    public Id: number;
    public Name: string;
    public CheckIn: Date;
    public CheckOut: Date;
    public Activated: boolean;
    public HotelName: string;
    public UniqueKey: string;

    constructor(
        id: number,
        name: string,
        checkIn: Date,
        checkOut: Date,
        activated: boolean,
        hotelName: string,
        uniqueKey: string) {
        this.Id = id;
        this.Name = name;
        this.CheckIn = checkIn;
        this.CheckOut = checkOut;
        this.Activated = activated;
        this.HotelName = hotelName;
        this.UniqueKey = uniqueKey;
    }
}