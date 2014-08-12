(function() {
  $(function() {
    return $(document).ready(function() {
      $(".md-header").css('height', '35px');
      return $("[name='deleteTag']").on('click', function(e) {
        var parent1, parent2;
        e.stopPropagation();
        e.preventDefault();
        parent1 = $(this).parent();
        parent2 = parent1.parent();
        return parent2.remove();
      });
    });
  });

}).call(this);

//# sourceMappingURL=setting.js.map
