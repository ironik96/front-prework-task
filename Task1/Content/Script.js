$("#segment-edit").click(function () {
    const selectedText = $("#MainContent_ProfileTypeList").find("option:selected").text()
    $("#MainContent_SegmentInputBox").val(selectedText);
    }
)