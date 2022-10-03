import React, { Component } from 'react';
import {
    Button,
    Badge,
    Modal,
    ModalBody,
    ModalHeader,
    ModalFooter,
    Form,
    FormGroup,
    Label,
    Input,
    Col
} from 'reactstrap';

export class MemberListing extends Component {
    static displayName = MemberListing.name;

    constructor(props) {
        super(props);
        this.state = {
            members: [],
            loading: true,
            modal: false,
            selectedMember: {},
            selectedMemberBilling: '',
            selectedMemberShipping: '',
        };

        this.toggleModal = this.toggleModal.bind(this);
    }

    toggleModal() {
        this.setState({
            modal: !this.state.modal
        });
    }

    closeMember() {
        this.setState({
            selectedMember: {}
        });
    }

    componentDidMount() {
        this.populateMemberData();
    }

    constructAddress(address) {
        let formatedAddress = '';
        if (address.address1 !== '' && address.address1 !== null) {
            formatedAddress += address.address1;
        }
        if (address.address2 !== '' && address.address2 !== null) {
            formatedAddress += ',\n' + address.address2;
        }
        if (address.city !== '' && address.city !== null) {
            formatedAddress += ',\n' + address.city;
        }
        if (address.stateProvince !== '' && address.stateProvince !== null) {
            formatedAddress += ',\n' + address.stateProvince;
        }
        if (address.zipCode !== '' && address.zipCode !== null) {
            formatedAddress += ' - ' + address.zipCode;
        }
        if (address.country !== '' && address.country !== null) {
            formatedAddress += '\n' + address.country
        }

        return formatedAddress;
    }

    render() {
        return (
            <div>
                <h1 id='tableLabel'>Member Listing</h1>
                <p>Member data fetched from NoviAMS API</p>
                {this.state.loading == false && (
                    <table className='table table-striped' aria-labelledby='tableLabel'>
                        <thead>
                            <tr>
                                <th>Name</th>
                                <th>Email</th>
                                <th>Phone Number</th>
                                <th>Customer Type</th>
                                <th>Active</th>
                            </tr>
                        </thead>
                        <tbody>
                            {this.state.members.map(member =>
                                <tr key={member.id}>
                                    <td>
                                        <Button color='link' onClick={async () => {
                                            const response = await fetch('members/' + member.id);
                                            const data = await response.json();
                                            const billing = this.constructAddress(data.billingAddress);
                                            const shipping = this.constructAddress(data.shippingAddress);
                                            this.setState({ selectedMember: data, modal: true, selectedMemberBilling: billing, selectedMemberShipping: shipping });
                                        }}>
                                            {member.name}
                                        </Button>
                                    </td>
                                    <td>{member.email}</td>
                                    <td>{member.phoneNumber}</td>
                                    <td>{member.customerType}</td>
                                    {member.isActive === true &&
                                        <td>
                                            <Badge color='success' pill>Active</Badge>
                                        </td>
                                    }
                                    {member.isActive === false &&
                                        <td>
                                            <Badge color='warning' pill>Inactive</Badge>
                                        </td>
                                    }
                                </tr>
                            )}
                        </tbody>
                    </table>
                )}
                {this.state.loading == true && (
                    <p><em>Loading...</em></p>
                )}
                <Modal
                    isOpen={this.state.modal}
                    toggle={this.toggleModal}
                >
                    <ModalHeader toggle={this.toggleModal}>
                        {this.state.selectedMember.name}
                    </ModalHeader>
                    <ModalBody>
                        <Form>
                            <FormGroup row>
                                <Label for="memberEmail" sm={4}>Email</Label>
                                <Col sm={6}>
                                    <Input id="memberEmail"
                                        disabled
                                        defaultValue={this.state.selectedMember.email}
                                    />  
                                </Col>
                            </FormGroup>
                            <FormGroup row>
                                <Label for="memberPhone" sm={4}>Phone</Label>
                                <Col sm={6}>
                                    <Input id="memberPhone"
                                        disabled
                                        defaultValue={this.state.selectedMember.phoneNumber}
                                    />
                                </Col>
                            </FormGroup>
                            <FormGroup row>
                                <Label for="memberType" sm={4}>Customer Type</Label>
                                <Col sm={6}>
                                    <Input id="memberType"
                                        disabled
                                        defaultValue={this.state.selectedMember.customerType}
                                    />
                                </Col>
                            </FormGroup>
                            <FormGroup row>
                                <Label for="memberType" sm={4}>Billing Address</Label>
                                <Col sm={6}>
                                    <Input id="memberType"
                                        type='textarea'
                                        defaultValue={this.state.selectedMemberBilling}
                                        rows="4"
                                        disabled
                                    />
                                </Col>
                            </FormGroup>
                            <FormGroup row>
                                <Label for="memberType" sm={4}>Shipping Address</Label>
                                <Col sm={6}>
                                    <Input id="memberType"
                                        type='textarea'
                                        defaultValue={this.state.selectedMemberShipping}
                                        rows="4"
                                        disabled
                                    />
                                </Col>
                            </FormGroup>
                            <FormGroup row>
                                <Col sm={6}>
                                        {this.state.selectedMember.isActive === true &&
                                        <td>
                                            <Badge color='success' pill>Active</Badge>
                                        </td>
                                        }
                                        {this.state.selectedMember.isActive === false &&
                                        <td>
                                            <Badge color='warning' pill>Inactive</Badge>
                                        </td>
                                    }
                                </Col>
                            </FormGroup>
                        </Form>
                    </ModalBody>
                    <ModalFooter>
                        <Button color='danger' onClick={this.toggleModal}>Close</Button>
                    </ModalFooter>
                </Modal>
            </div>
        );
    }

    async populateMemberData() {
        const response = await fetch('members');
        const data = await response.json();
        this.setState({ members: data, loading: false });
    }
}