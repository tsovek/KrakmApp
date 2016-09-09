export class Partner {
    constructor(
        public id: number,
        public name: string,
        public latitude: number,
        public longitude: number,
        public description: string,
        public phone: string,
        public commission: number,
        public promotionKind: number,
        public startPromotion: Date,
        public endPromotion: Date,
        public imageUrl: string) { }
}