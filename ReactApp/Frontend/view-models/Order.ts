//-------------------------------------------------------------------------------------------
// <copyright file="Order.ts" website="Patrikduch.com">
//     Copyright 2019 (c) Patrikduch.com
// </copyright>
// <author>Patrik Duch</author>
// View model for representation of orders
//--------------------------------------------------------------------------------------------

export default class Order {
    private id: number;
    private creationDate: Date;
    private customerId :number;
    private products: any;
    private discount: number;

    constructor(id: number, creationDate: Date, customerId:number, products:any, discount: number) {
        this.id = id;
        this.creationDate = creationDate;
        this.customerId = customerId;
        this.products = products;
        this.discount = discount;
    }

    get getId() {
        return this.id;
    }

    get GetCreationDate() {
        return this.creationDate;
    }

    set SetCreationDate(value: any) {

        this.creationDate = value;
    }

    get getCustomerId() {
        return this.customerId;
    }

    get getProducts() {
        return this.products;
    }

    get getProductDiscount() {
        return this.discount;
    }
}