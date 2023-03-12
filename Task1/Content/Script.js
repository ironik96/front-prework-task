const modalContent = {
    profile: {
        title: 'Segment',
        placeholder: 'New segment name',
        selectedText: () => $("#MainContent_ProfileTypeList").find("option:selected").text()
    },
    account: {
        title: 'Account',
        placeholder: 'New account name',
        selectedText: () => $("#MainContent_AccountTypeList").find("option:selected").text()
    },
    card: {
        title: 'Card',
        placeholder: 'New card name',
        selectedText: () => $("#MainContent_CardTypeList").find("option:selected").text()
    }
}


$('#addModal').on('show.bs.modal', function (event) {
    
    $("#MainContent_TypeInputBox_Add").val("");

    const button = $(event.relatedTarget) 
    const type = button.data('type') 
    
    const modal = $(this)
    $('#fields-holders').find('input').val(type)
    modal.find('.modal-title').text(modalContent[type].title)
    modal.find('.modal-body input').attr("placeholder", modalContent[type].placeholder)
})

$('#editModal').on('show.bs.modal', function (event) {
    const button = $(event.relatedTarget)
    const type = button.data('type')

    const selectedText = modalContent[type].selectedText();
    $("#MainContent_TypeInputBox_Edit").val(selectedText);

    const modal = $(this)
    $('#fields-holders').find('input').val(type)
    modal.find('.modal-title').text(modalContent[type].title)
    modal.find('.modal-body input').attr("placeholder", modalContent[type].placeholder)
})

$('#deleteModal').on('show.bs.modal', function (event) {

    const button = $(event.relatedTarget)
    const type = button.data('type')

    const modal = $(this)
    $('#fields-holders').find('input').val(type)
    modal.find('.modal-title').text(`Delete ${modalContent[type].title}?`)
})