export class EmitEvent {
    constructor(public name: BusEvents, public value?: any) { }
};

export enum BusEvents {
    feedRemoved
}