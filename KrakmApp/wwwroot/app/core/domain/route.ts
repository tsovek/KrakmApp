export class Route {
    constructor(
        public id: number,
        public name: string,
        public description: string,
        public active: boolean,
        public points: RoutePoint[]) {}
}

export class RoutePoint {
    constructor(
        public order: number,
        public name: string,
        public description: string,
        public imageUrl: string,
        public markerUrl: string) { }
}