//---------------------------------------------------------------------------------------
// <copyright file="Order-List.tsx" website="Patrikduch.com">
//     Copyright 2019 (c) Patrikduch.com
// </copyright>
// <author>Patrik Duch</author>
// Order list which consists table with data manipulations (Redux connected component)
//----------------------------------------------------------------------------------------

// React dependency
import * as React from 'react';
// Title of page (h2 heading)
import ListTitle from '../common/title/Page-Title';
import ListContainer from '../common/crud/read/List-Container';
import { ListItemType } from '../../typescript/enums/crud/List-Item-Type';
import { toCrudData } from '../../helpers/components/crudHelper';


interface IOrderListProps {
    actions:{
        getOrders: Function // fetching list of orders
    }
    orders: any, // list of orders
}


export default class OrdertList extends React.Component<IOrderListProps, any> {

    componentWillMount() {
        this.props.actions.getOrders();
    }

    render(){
        return (
            <div>
                <ListContainer
                data={ toCrudData(this.props.orders) }
                updateMethod = {null}
                deleteMethod ={null} 
                columnNames = {['#','Datum objednávky', 'Zákazník','Výrobky']}
                emptyError = 'Seznam objednávek je prázdný'
                type={ListItemType.Order}
                />
            </div>
        )
    }

}