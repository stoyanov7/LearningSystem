function createSlug(nameElementId, slugElementId) {
    $(`#${nameElementId}`).change(function (e) {
        let value = $(this).val();
        let slug = value.toLowerCase().replace(/ /g, "-");
        $(`#${slugElementId}`).val(slug);
    });
}