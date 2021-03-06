//---------------------------------------------------------------------------------------
// <copyright file="List-Body.tsx" website="Patrikduch.com">
//     Copyright 2019 (c) Patrikduch.com
// </copyright>
// <author>Patrik Duch</author>
// Container for list content
//----------------------------------------------------------------------------------------

// React dependency
import * as React from 'react';
// Styled helper
import styled from 'styled-components';
import { Table } from 'reactstrap';
import ListHeadings from './List-Headings';
import ListBody from '../read/List-Body';

// Container that wrappps list of customers
const Container = styled.div`
  //  margin-top: 1.0vh;
`;

export default (props:any) => {
    return (
       <Container>
           <Table >
                <ListHeadings columns={props.columnNames} />
                <ListBody 
                    type={props.type} 
                    data={props.data} 
                    emptyError={props.emptyError}
                    stats ={props.stats}
                    crud={props.crud}
                    deleteMethod={props.deleteMethod}
                    updateMethod={props.updateMethod}
                    />
           </Table>
       </Container>
    )
}


