export class Client {
    constructor(
        public id: number,
        public name: string,
        public checkIn: Date,
        public checkOut: Date,
        public activated: boolean,
        public hotelName: string,
        public uniqueKey: string) { }
}