<script>
    $(document).ready(function () {
        $('#imageModal').on('show.bs.modal', function (e) {
            var imageUrl = $(e.relatedTarget).data('image-url');
            $('#modalImage').attr('src', imageUrl);
        });
    });
</script>
