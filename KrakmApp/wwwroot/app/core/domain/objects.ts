export class Objects {
    constructor(public objects: Array<ObjectGroup>) { }
}

export class ObjectGroup {
    constructor(public type: string, public singleObjects: Array<SingleObject>) { }
}

export class SingleObject {
    constructor(
        public idInType: number,
        public name: string,
        public description: string,
        public imageUrl: string,
        public latitude: number,
        public longitude: number) { }
}

export class SortableObject {
    constructor(public object: SingleObject, public objType: string, public order: number) { }
}