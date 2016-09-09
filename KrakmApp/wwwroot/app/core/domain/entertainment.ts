export class Entertainment {
    constructor(
        public id: number,
        public name: string,
        public latitude: number,
        public longitude: number,
        public imageUrl: string,
        public payable: boolean,
        public description: string,
        public adress: string) { }
}