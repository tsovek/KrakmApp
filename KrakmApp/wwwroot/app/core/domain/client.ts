export class Client {
    public Id: number;
    public Name: string;
    public CheckIn: Date;
    public CheckOut: Date;
    public Activated: boolean;
    public HotelId: number;
    public UniqueKey: string;

    constructor(
        id: number,
        name: string,
        checkIn: Date,
        checkOut: Date,
        activated: boolean,
        hotelId: number,
        uniqueKey: string) {
        this.Id = id;
        this.Name = name;
        this.CheckIn = checkIn;
        this.CheckOut = checkOut;
        this.Activated = activated;
        this.HotelId = hotelId;
        this.UniqueKey = uniqueKey;
    }
}