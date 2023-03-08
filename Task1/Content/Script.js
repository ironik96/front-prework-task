$("#segment-edit").click(function () {
    const selectedText = $("#MainContent_ProfileTypeList").find("option:selected").text()
    $("#MainContent_SegmentInputBox_Edit").val(selectedText);
    }
)

$("#segment-add").click(function () {
    $("#MainContent_SegmentInputBox_Add").val("");
}
)