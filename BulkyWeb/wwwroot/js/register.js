$(document).ready(function () {
    var selectedRole = null;
    $('#Input_Role').on("change", function () {
        selectedRole = $('#Input_Role').val();
        if (selectedRole == "Company") {
            $('#Input_CompanyId').removeClass('d-none');
        } else {
            $('#Input_CompanyId').addClass('d-none');
            $("#Input_CompanyId option").prop("selected", function () {
                return this.defaultSelected;
            });
        }
    });
});