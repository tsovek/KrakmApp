export class Banner {
    constructor(
        public id: number,
        public name: string,
        public description: string,
        public startPromotion: Date,
        public endPromotion: Date,
        public imageUrl: string) { }
}