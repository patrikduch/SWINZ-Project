//-----------------------------------------------------------------------
// <copyright file="Customer-Page.tsx" website="Patrikduch.com">
//     Copyright 2019 (c) Patrikduch.com
// </copyright>
// <author>Patrik Duch</author>
// Customer page
//-----------------------------------------------------------------------

import * as React from 'react';
import CustomerListComp from '../../components/customers/Customers-Crud-List-Container';

import { Container } from 'reactstrap';

export default  () => {
    return (
        <Container>
            <CustomerListComp />
        </Container>
    )
}