$ ->
  $(document).ready () ->
    $(".md-header").css('height', '35px')
    $("[name='deleteTag']").on 'click', () ->
      alert "cliked"