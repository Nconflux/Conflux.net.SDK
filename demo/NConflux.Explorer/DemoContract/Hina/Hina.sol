// SPDX-License-Identifier: MIT
pragma solidity >=0.6.0;

contract Hina {
    string private content;
    address private sender;

    constructor() public {}

    event LeaveMessage(address from, string message, uint256 payed);
    event GasInfo(uint256 msgGas);
 
    function getContent() public view returns (string memory) {
        return content;
    }
 
    function getContentWithIndentifier() public view returns (string memory message) {
        message = content;
    }

    function leaveMessage(string calldata _message) public payable {
        content = _message;
        sender = msg.sender;
        emit LeaveMessage(sender, content, msg.value);
        emit GasInfo(gasleft());
    }

    function messageWithSender() public view returns (string memory message, address) {
        return (content, sender);
    }

    function addresses() public view returns (address[] memory ret) {
        ret = new address[](4);
        ret[0] = msg.sender;
        ret[1] = sender;
        ret[2] = tx.origin;
        ret[3] = address(this);
    }
}
