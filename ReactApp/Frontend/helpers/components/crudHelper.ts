import Product from "../../view-models/Order";
import Order from "../../view-models/Order";
import ListItemObject from "../types/List-Item-Object";


// Transformation from geteched data into view modal data

export function toCrudData(collection: any) {

    let list = new ListItemObject<Order>();

    for(let i = 0; i<Object.keys(collection).length; i++) {

        const item = new Order(collection[i].id, collection[i].creationDate,collection[i].customerId,collection[i].products, collection[i].discount);
        list.objects.push(item);
    }
    
    return list;
}